namespace SportsBettingSystem.Tests.IntegrationTests
{
	using Microsoft.AspNetCore.Mvc;
	using Web.Controllers;

	public class HomeControllerTests
	{
		private HomeController homeController;

		[OneTimeSetUp]
		public void Setup()
			=> this.homeController = new HomeController();

		[Test]
		public void ErrorShouldReturnCorrectView()
		{
			int statusCode = 400;

			var result = this.homeController.Error(statusCode);

			Assert.IsNotNull(result);

			var viewResult = result as ViewResult;
			
			Assert.IsNotNull(viewResult);
		}
		[Test]
		public void ErrorShouldReturnCorrectViewV2()
		{
			int statusCode = 401;

			var result = this.homeController.Error(statusCode);

			Assert.IsNotNull(result);

			var viewResult = result as ViewResult;

			Assert.IsNotNull(viewResult);
		}
		[Test]
		public void ErrorShouldReturnCorrectViewV3()
		{
			int statusCode = 500;

			var result = this.homeController.Error(statusCode);

			Assert.IsNotNull(result);

			var viewResult = result as ViewResult;

			Assert.IsNotNull(viewResult);
		}
	}
}
