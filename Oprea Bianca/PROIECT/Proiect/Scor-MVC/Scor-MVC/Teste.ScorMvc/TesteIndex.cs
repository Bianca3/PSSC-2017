using Moq;
using Scor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Models;
using Xunit;

namespace Teste.ScorMvc
{
    public class TesteIndex
    {
		[Fact]
		public void TrebuieSaAfisezeEchipeleSiScorul()
		{
			var mockReadRepo = new Mock<ReadRepository>();
			var mockWriteRepo = new Mock<WriteRepository>();

			mockReadRepo.Setup(mock => mock.ExistaFisier(It.IsAny<string>())).Returns(true);
			mockReadRepo.Setup(_ => _.CitesteContinutFisier(It.IsAny<string>())).Returns("[{\"Id\":\"00e14401-a9e8-4b72-b73d-896e9822f614\",\"Echipa1\":\"Poli\",\"Echipa2\":\"FCSB\",\"Data\":\"2017-11-20T09: 48:54.2800764+02:00\",\"GoluriEchipa1\":8,\"GoluriEchipa2\":4}]");

			var controller = new HomeController(mockReadRepo.Object, mockWriteRepo.Object);

			var model = (MeciModel)(((System.Web.Mvc.ViewResult)controller.Index()).Model);
			Assert.Equal("Poli", model.Echipa1);
			Assert.Equal(8, model.Goluri1);
		}
    }
}
