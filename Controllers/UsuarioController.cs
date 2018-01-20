using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetoForum.Models;

namespace ProjetoForum.Controllers
{
    public class UsuarioController:Controller
    {
        DAOUsuario dao = new DAOUsuario();

        [Route ("api/[controller]")]

        [HttpPost]
        public IActionResult Post(Usuario usuario){
            bool resultado = dao.Cadastro(usuario);

            return CreatedAtRoute("ExibeUsuario",new{Id=usuario.Id},usuario);
        }

        [HttpGet("{id}",Name="ExibeUsuario")]
        public Usuario Get(int Id){
            return dao.Listar().Where(x=>x.Id==Id).FirstOrDefault();
        }
    }
}