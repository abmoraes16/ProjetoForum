using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoForum.Models
{
    public class DAOUsuario
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        string stringConexao = @"Data Source=.\SqlExpress;Initial Catalog=ProjetoForum;user id=sa;password=senai@123";

        public List<Usuario> Listar(){
            List<Usuario> ListaUsuario = null;

            try{
                conn = new SqlConnection(stringConexao);
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"select * from usuario";
                cmd.Connection = conn;

                reader = cmd.ExecuteReader();

                while(reader.Read()){
                    ListaUsuario.Add(new Usuario(){
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Login = reader.GetString(2),
                        Senha = reader.GetString(3),
                        DataCadastro = reader.GetDateTime(4)
                    });
                }

            }catch(SqlException ex){
                throw new Exception(ex.Message);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            finally{
                conn.Close();
            }

            return ListaUsuario;
    }

    public bool Cadastro(Usuario Usuario){
        bool resultado = false;

        try{
            conn = new SqlConnection(stringConexao);
            conn.Open();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"insert into usuario(nome,login,senha) values(@Nome,@Login,@Senha)";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@Nome",Usuario.Nome);
            cmd.Parameters.AddWithValue("@Login",Usuario.Login);
            cmd.Parameters.AddWithValue("@Senha",Usuario.Senha);
            
            int linhas = cmd.ExecuteNonQuery();

            if(linhas>0){
                resultado=true;
            }
        }
        catch(SqlException ex){
            throw new Exception(ex.Message);
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
        finally{
            cmd.Parameters.Clear();
            conn.Close();
        }

        return resultado;
    }

    public bool Atualizar(Usuario Usuario){
        bool resultado = false;

        try{
            conn = new SqlConnection(stringConexao);
            conn.Open();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"update usuario set nome=@Nome,login=@Login,senha=@Senha where id=@Id";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@Nome",Usuario.Nome);
            cmd.Parameters.AddWithValue("Login",Usuario.Login);
            cmd.Parameters.AddWithValue("@Senha",Usuario.Senha);
            cmd.Parameters.AddWithValue("@Id",Usuario.Id);

            int linhas = cmd.ExecuteNonQuery();

            if(linhas>0){
                resultado = true;
            }

        }catch(SqlException ex){
            throw new Exception(ex.Message);
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
        finally{
            cmd.Parameters.Clear();
            conn.Close();
        }

        return resultado;
    }

    public bool Excluir(int id){
        bool resultado = false;

        try{
            conn = new SqlConnection(stringConexao);
            conn.Open();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from usuario where id=@Id";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@Id",id);

            int linhas = cmd.ExecuteNonQuery();            

            if(linhas>0){
                resultado = true;
            }

        }catch(SqlException ex){
            throw new Exception(ex.Message);
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
        finally{
            cmd.Parameters.Clear();
            conn.Close();
        }

        return resultado;
    }
        
        
    }
}