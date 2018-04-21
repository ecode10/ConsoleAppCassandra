using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCassandra
{
    class Program
    {
        static void Main(string[] args)
        {
            //Conectando o keyspace com o cluster local
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("demo");

            //inserindo valores
            session.Execute("insert into users (lastName, firstName, age, city, email) values ('Junior', 'Mauricio', 35, 'Goiania', 'mauricio.junior@gmail.com')");

            //lendo valores
            Row result = session.Execute("SELECT lastName, fistName, age, city, email FROM Users WHERE lastName = 'Junior'").First();
            Console.WriteLine("{0} {1}", result["firstName"], result["age"]);

            //atualizando valores
            session.Execute("update Users set age = 36 where lastName = 'Junior'");
            result = session.Execute("select * from Users where lastName='Junior'").First();
            Console.WriteLine("{0} {1}", result["firstName"], result["lastName"]);

            //deletando valores
            session.Execute("DELETE from Users where lastName='Junior'");

            RowSet rows = session.Execute("SELECT * FROM Users");
            foreach (Row row in rows)
            {
                Console.WriteLine("{0} {1}", row["firstName"], row["lastName"]);
            }

            Console.ReadLine();

        }
    }
}
