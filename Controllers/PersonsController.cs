using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace webapicore.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
   
    //[Route("api/[Persons]")]

    public class PersonsController : ControllerBase
    {
        public PersonsController(Data.DatabaseContext database)
        {
            Db = database;
        }

        protected Data.DatabaseContext Db { get;}
        //https://localhost:44343/api/persons
        [HttpGet]
        //اسم تابع مهم نیست
        public List<Person> GetPerson1()
        {
            List<Person> lisPerson = new List<Person>();
         
            Person p = new Person();
            p.Id = 1;
            p.Name = "hamid";
            lisPerson.Add(p);

            return lisPerson;
        }

        //https://localhost:44343/api/persons
        [HttpPost]
        public Person Googoli(Person p)
        {

            return new Person() { Id = 1, Name = p.Name };
        }

        //https://localhost:44343/api/persons/a/b
        [HttpGet]
        [Route("a/b")]
        //or
        //[HttpGet("a/b")]
        public Person GetPerson2()
        {
            return new Person() { Id = 10, Name = "hamid" };
        }

        //https://localhost:44343/a/b
        [HttpGet]
        [Route("/a/b")]
        public Person GetPerson3()
        {
            return new Person() { Id = 10, Name = "hamid" };
        }


        //https://localhost:44343/api/persons/Async

        [HttpGet("Async")]
        public
            async
           Task<Person>
            GetPersonAsync()
        {
            Person p = new Person();
            await
                Task.Run(() =>
                {
                    p = new Person() { Id = 10, Name = "hamid" };

                }
                );
            return p;
        }


        //https://localhost:44343/api/persons/DBAsinc

        [HttpGet("DBAsinc")]
        public
            async
           Task<Data.Person>
            GetPersonDBAsinc()
        {
            var p = await Db.Persons.FirstOrDefaultAsync();
         
            return p;
        }
        //https://localhost:44343/api/persons/DBAsinc/33

        [HttpGet("DBAsinc/{Id}")]
        public
            async
           Task<Data.Person>
            GetPersonDBAsinc(int id)
        {
            var p = await Db.Persons.Where(C=>C.Id==id).FirstOrDefaultAsync();

            return p;
        }

        //https://localhost:44343/ok
        [HttpGet]
        [Route("ok")]
        public ActionResult<Person> GetPersonOk()
        {
            return new Person() { Id = 10, Name = "hamid" };
        }


       

    }
}
