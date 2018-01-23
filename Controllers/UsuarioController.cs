using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetoForum.Models;

namespace ProjetoForum.Controllers
{
    [Route ("api/[controller]")]
    public class UsuarioController:Controller
    {
        DAOUsuario dao = new DAOUsuario();

        [HttpGet (Name="Listar")]
        public IEnumerable<Usuario> Get(){
            return dao.Listar();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario){
            bool resultado = dao.Cadastro(usuario);

            return CreatedAtRoute("ExibeUsuario",new{Id=usuario.Id},usuario);
        }

        [HttpGet("{Id}",Name="ExibeUsuario")]
        public IActionResult Get(int Id){
            var rs = new JsonResult(dao.Listar().Where(x=>x.Id==Id).FirstOrDefault()); //retorna primeiro dado que encontrar da lista que tiver o id informado
            rs.ContentType = "application/json";

            if(rs.Value==null){
                rs.StatusCode = 204;
                rs.Value = $"Resultado para id: {Id} não retornou dados.";
            }
            else
            {
                rs.StatusCode = 200;
            }

            return Json(rs);
        }

        [HttpPut]
        public bool Put([FromBody]Usuario usuario){
            return dao.Atualizar(usuario);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            dao.Excluir(id);
            return RedirectToRoute("Listar");
        }

    }
}