using Moq;
using Scor.Evenimente;
using Scor.Model;
using Scor.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Model
{
	public class TesteMeci//: IClassFixture<InitTesteMeci>
	{
		private Mock<ProcesatorEveniment> _mockProcesatorProgramareMeci;
		private MagistralaEvenimente _magistrala;
		private MeciDto _meciDto;

		//public TesteMeci(InitTesteMeci init)
		//{
		//	_meciDto = new MeciDto() { Id= Guid.NewGuid(), Echipa1 = "Poli", Echipa2 = "FCSB" };
		//	_init = init;
		//}
		public TesteMeci()
		{
			_meciDto = new MeciDto() { Id = Guid.NewGuid(), Echipa1 = "Poli", Echipa2 = "FCSB" };
			_magistrala = new MagistralaEvenimente();
			_magistrala.InregistreazaProcesatoareStandard();
			_mockProcesatorProgramareMeci = new Mock<ProcesatorEveniment>();
			_magistrala.InregistreazaProcesator(TipEveniment.ProgramareMeci, _mockProcesatorProgramareMeci.Object);
			_magistrala.InchideInregistrarea();
		}

		[Fact]
		public void TrebuieSaSetezeProprietatileMeciului()
		{
			//actiune
			var meci = new Meci(_meciDto, _magistrala);

			Assert.Equal("Poli", meci.Echipa1);
			Assert.Equal("FCSB", meci.Echipa2);
			Assert.Equal(StareMeci.Programat, meci.Stare);
			Assert.Equal(0, meci.GoluriEchipa1.NumarGoluri);
			Assert.Equal(0, meci.GoluriEchipa2.NumarGoluri);
		}

		[Fact]
		public void TrebuieSaVerifceIdMeci()
		{
			var meciDtoFaraId = new MeciDto() { Echipa1 = "Poli", Echipa2 = "FCSB" };
			Assert.Throws<Exception>(() =>
			{
				new Meci(meciDtoFaraId, _magistrala);
			});
		}

		[Fact]
		public void TrebuieSaGenerezeEvenimentNouProgramareMeci()
		{
			var meci = new Meci(_meciDto, _magistrala);

			Assert.NotEmpty(meci.EvenimenteNoi);
			var e = meci.EvenimenteNoi.First();
			Assert.Equal(TipEveniment.ProgramareMeci, e.Tip);
			Assert.IsType<EvenimentGeneric<MeciDto>>(e);
			Assert.Equal(_meciDto.Id, e.IdRadacina);
			Assert.Equal("Poli", ((EvenimentGeneric<MeciDto>)e).Detalii.Echipa1);
		}

		[Fact]
		public void TrebuieSaPubliceEvenimentProgramareMeci()
		{
			var meciId = Guid.NewGuid();
			var meciDto = new MeciDto() { Id = meciId };
			var meci = new Meci(meciDto, _magistrala);

			_mockProcesatorProgramareMeci.Verify(_ => 
					_.Proceseaza(It.Is<Eveniment>(e=>e.IdRadacina==meciId)), Times.Once);

		}

		[Fact]
		public void SeSchimbaStaeaMeciuluiLaStart()
		{
			var meci = new Meci(_meciDto, _magistrala);
			meci.Start();

			Assert.Equal(StareMeci.InDesfasurare, meci.Stare);
		}

		[Fact]
		public void SeVerificaStareaMeciuluiInainteDeStart()
		{
			var meci = new Meci(_meciDto, _magistrala);
			meci.Start();

			var ex = Assert.Throws<InvalidOperationException>(()=>meci.Start());
			Assert.Equal("Meciul nu este in starea corecta", ex.Message);
		}
	}
}
