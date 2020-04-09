using System;
using SQLite;

namespace MySpectrumCodingTest.Models
{
    public class Entity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
    }
}
