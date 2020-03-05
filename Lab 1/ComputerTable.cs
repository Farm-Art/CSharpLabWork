using System;

namespace Lab_1
{
    public class ComputerTable
    {
        public enum Key
        {
            Model, Printer, Price, Material
        }
        public string Model { get; set; }
        public bool HasPrinter { get; set; }
        public decimal Price { get; set; }
        public string Material { get; set; }
        
        public ComputerTable(string model = "", bool hasPrinter = false,
                             decimal price = 0.0m, string material = "")
        {
            Model = model;
            HasPrinter = hasPrinter;
            Price = price;
            Material = material;
        }

        public override string ToString()
        {
            return String.Format("{0} made of {3}, printer stand {1}included, ${2}",
                Model, (HasPrinter ? "" : "not "), Price, Material);
        }
    }
}