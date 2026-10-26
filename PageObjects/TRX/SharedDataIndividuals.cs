using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Fields;

namespace TA.Playwright.PageObjects
{
    public interface ISharedDataIndividuals
    {
        List<IndividualFields> Customers { get; }

        void ClearIndividuals();
    }

    public class SharedDataIndividuals : ISharedDataIndividuals
    {
        public List<IndividualFields> Customers { get; } = new List<IndividualFields>();

        public void ClearIndividuals()
        {
            Customers.Clear();
        }
    }
}