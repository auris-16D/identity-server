using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using BddApiTests.Client;

namespace BddApiTests.Steps
{
    [Binding]
    public class CalculatorStepDefinitions
    {

        private decimal firsNumber = 0;
        private decimal secondNumber = 0;
        private decimal result = 0;
        private HttpClient apiClient;
       
       // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

       private readonly ScenarioContext _scenarioContext;

       public CalculatorStepDefinitions(ScenarioContext scenarioContext)
       {
           _scenarioContext = scenarioContext;
       }

       [Given("the first number is (.*)")]
       public async Task GivenTheFirstNumberIs(int number)
       {
            apiClient = await AuthenticatedClient.Get();

            this.firsNumber = number;
       }

       [Given("the second number is (.*)")]
       public async Task GivenTheSecondNumberIs(int number)
       {
            var budgets = await apiClient.GetAsync("http://localhost:6001/api/v1/budgets/17/principle/6685e0dd-653c-407a-88f5-ea3cf643890e");
            this.secondNumber = number;
        }
        
       [When("the two numbers are added")]
       public void WhenTheTwoNumbersAreAdded()
       {
           this.result = this.firsNumber + this.secondNumber;
       }

       [Then("the result should be (.*)")]
       public void ThenTheResultShouldBe(int result)
       {
           Assert.AreEqual(this.result, result);
       }
    }
}
