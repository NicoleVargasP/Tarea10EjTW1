using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tarea10EjTW1.Models;
namespace Tarea10EjTW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tarea10EjController : ControllerBase
    {
        //1) Ejercicio 6 Buscar en lista
        [HttpGet("buscar/{animal}")]
        public IActionResult buscarAnimal(string animal)
        {
            var animales = new List<string> { "Gato", "Perro", "Loro", "Murcielago", "Panda" };
            if (animales.Contains(animal, StringComparer.OrdinalIgnoreCase))
            {
                return Ok(animal);
            }
            return NotFound(new { mensaje = $"'{animal}' no esta en la lista." });
        }

        //2) Ejercicio 7 Filtrar numeros pares 
        [HttpPost("pares")]
        public IActionResult buscarPares([FromBody] List<int> numeros)
        {
            List<int> pares = new List<int>();
            foreach (int numero in numeros)
            {
                if (numero % 2 == 0)
                {
                    pares.Add(numero);
                }
            }
            return Ok(pares);
        }

        //3) Ejercicio 8 Diccionario de traducciones 
        [HttpGet("traducir/{palabra}")]
        public IActionResult traducir(string palabra)
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
             {
                { "hello", "hola" },
                { "book", "libro" },
                { "cat", "gato" },
                { "cellphone", "celular" },
                { "dog", "perro" },
                { "bag", "mochila" },
                { "lemon", "limon" },

             };
            if (diccionario.TryGetValue(palabra, out string traduccion))
            {
                return Ok(new { palabra, traduccion });
            }
            return NotFound("La palabra no se encontro");
        }
        //4) Ejercicio 9 Contador de Palabras

        [HttpPost("contarpalabras")]
        public IActionResult contarPalabras([FromBody] string texto)
        {

            if (string.IsNullOrWhiteSpace(texto))
            {
                return BadRequest("El texto esta vacio");
            }
            string[] palabras = texto.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (palabras.Length == 0)
            {
                return NotFound("No hay palabras en el texto");
            }
            int cantidad = palabras.Length;
            return Ok("El texto tiene: " + cantidad + " palabras");
        }

        //5) Ejercicio 11 Lista de Productos
        [HttpGet("productos")]
        public IActionResult GetProductos()
        {
            var productos = new List<Producto>
    {
        new Producto { Id = 1, Nombre = "Laptop", Precio = 3500 },
        new Producto { Id = 2, Nombre = "Celular", Precio = 2500 },
        new Producto { Id = 3, Nombre = "Audifonos", Precio = 800 }
    };

            return Ok(productos);
        }
        //6) Ejercicio 12 Lista de Empleados con herencia
        [HttpGet("empleados")]
        public IActionResult GetEmpleados()
        {
            var empleados = new List<Empleado>
    {
        new EmpleadoNormal { Id = 1, Nombre = "Carlos" },
        new Gerente { Id = 2, Nombre = "Ana" },
        new EmpleadoNormal { Id = 3, Nombre = "Luis" }
    };

            var resultado = empleados.Select(e => new
            {
                e.Id,
                e.Nombre,
                Cargo = e.GetCargo()
            });

            return Ok(resultado);
        }
        //7) Ejercicio 6 Agregar ítems a una lista genérica
        [ApiController]
        [Route("[controller]")]
        public class ListaItemsController : ControllerBase
        {
            [HttpPost("agregaritem")]
            public IActionResult AgregarItem([FromBody] string item)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    return BadRequest("No hay items");
                }

                ListaItems.Items.Add(item);

                return Ok(new
                {
                    mensaje = $"Item '{item}' agregado",
                    listaActual = ListaItems.Items
                });
            }

            [HttpGet("veritems")]
            public IActionResult VerItems()
            {
                return Ok(ListaItems.Items);
            }
        }
        //8) //Ejercicio 21 Validar Edad
        [HttpGet("validaredad/{edad}")]
        public IActionResult ValidarEdad(int edad)
        {
            if (edad < 18)
            {
                return BadRequest(new { mensaje = "La edad debe ser mayor o igual a 18 años." });
            }

            return Ok(new { mensaje = $"Edad válida: {edad}" });
        }
        //9) //Ejercicio 22 Dividir con Try-Catch
        [HttpGet("dividir/{a}/{b}")]
        public IActionResult Dividir(int a, int b)
        {
            try
            {
                int resultado = a / b;
                return Ok(new { mensaje = $"El resultado de {a} / {b} es {resultado}" });
            }
            catch (DivideByZeroException)
            {
                return BadRequest("No se puede dividir entre cero.");
            }
        }
        // 10) //Ejercicio 41 Generador de Contraseñas
        [HttpGet("contrasenia/{longitud}")]
        public IActionResult GenerarPassword(int longitud)
        {
            if (longitud < 8)
            {
                return BadRequest("La longitud mínima es 8 caracteres.");
            }
            string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string minusculas = "abcdefghijklmnopqrstuvwxyz";
            string numeros = "0123456789";
            string simbolos = "!@#$%&*;:,.=?";

            string todos = mayusculas + minusculas + numeros + simbolos;

            Random random = new Random();
            var password = new System.Text.StringBuilder();

            for (int i = 0; i < longitud; i++)
            {
                int index = random.Next(todos.Length);
                password.Append(todos[index]);
            }

            return Ok(new { password = password.ToString() });
        }

    }
}
