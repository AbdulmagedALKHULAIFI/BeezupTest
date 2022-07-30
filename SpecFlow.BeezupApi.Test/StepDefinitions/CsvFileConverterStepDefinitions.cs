using BeezupApi.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlow.BeezupApi.Test.Models;
using System.Xml.Linq;

namespace SpecFlow.BeezupApi.Test.StepDefinitions
{
    [Binding]
    public sealed class CsvFileConverterStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FileConverterService _fileConverterService;

        public CsvFileConverterStepDefinitions(ScenarioContext scenarioContext, FileConverterService fileConverterService)
        {
            _scenarioContext = scenarioContext;
            _fileConverterService = fileConverterService;
        }

        [Given(@"Csv File")]
        public void GivenCsvFile(Table table)
        {
            string csvContent = string.Empty;

            csvContent = csvContent + String.Join(", ", table.Header.ToArray());
            csvContent = csvContent + "\n";

            foreach (var row in table.Rows)
            {
                var rowItems = row.Values.ToList();

                csvContent = csvContent +  String.Join(", ", rowItems.ToArray());
                csvContent = csvContent + "\n";
            }
            
            _scenarioContext.Add("CsvContent",csvContent);
        }

        [Given(@"csv delimiter (.*)")]
        public void GivenCsvDelimiter(string delimiter)
        {
            string csvContent = _scenarioContext.Get<string>("CsvContent");

            _scenarioContext["CsvContent"] = csvContent.Replace(",", delimiter);

            _scenarioContext.Add("Delimiter", delimiter);
        }


        [When(@"Convert file")]
        public void WhenConvertFile()
        {
            string csvContent = _scenarioContext.Get<string>("CsvContent");
            string delimiter = _scenarioContext.Get<string>("Delimiter");

            _scenarioContext.Add("OutputJson", _fileConverterService.ConvertCsvToJson(csvContent, delimiter));
            _scenarioContext.Add("OutputXml", _fileConverterService.ConvertCsvToXml(csvContent, delimiter));
        }

        [Then(@"Verify that the Csv is successfully converted to Json")]
        public void ThenVerifyThatTheCsvIsSuccessfullyConvertedToJson()
        {
            JsonFile expectedResult = new JsonFile()
            {
                Rows = new List<JsonRow>()
                {
                    new JsonRow("s1324", "My super product", "My super product it’s the best of the market", "1.21"),
                    new JsonRow("x5611", "My second super product", "My second super product it’s the best of the market", "7.43")
                }
            };


            string output = _scenarioContext.Get<string>("OutputJson");
            JsonFile outputJson = new JsonFile();
            outputJson.Rows = JsonConvert.DeserializeObject<List<JsonRow>>(output);

            //assert
            Assert.AreEqual(expectedResult, outputJson);
        }

        [Then(@"Verify that the Csv is successfully converted to Xml")]
        public void ThenVerifyThatTheCsvIsSuccessfullyConvertedToXml()
        {
            string outputXml = _scenarioContext.Get<string>("OutputXml");

            XDocument expectedResult = new XDocument(new XElement("MainBody",
                                          new XElement("level1",
                                              new XElement("level2", "Demo text"),
                                              new XElement("level2", "other text"))));
        }

    }
}