using Scor;
using Scor.Comenzi;
using Scor.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		private const string RootPath = @"C:\\Users\\Bianca\\Desktop\\";
		private ReadRepository _readRepo;
		private WriteRepository _writeRepo;

		public HomeController()
		{
			_readRepo = new ReadRepository(RootPath);
			_writeRepo = new WriteRepository(RootPath);
		}

		public HomeController(ReadRepository readRepo, WriteRepository writeRepo)
		{
			_readRepo = readRepo;
			_writeRepo = writeRepo;
		}

		public ActionResult Index()
		{
            
            var meciDto = _readRepo.ObtineMeciuri().First();
			return View(GenereazaModelView(meciDto));
		}

		private static MeciModel GenereazaModelView(MeciDto meciDto)
		{
			return new MeciModel()
			{
				Echipa1 = meciDto.Echipa1,
				Echipa2 = meciDto.Echipa2,
				Goluri1 = meciDto.GoluriEchipa1,
				Goluri2 = meciDto.GoluriEchipa2,
				MeciId = meciDto.Id
			};
		}

		[HttpPost]
		public ActionResult StartMeci(Guid meciId)
		{
			var meciDto = _readRepo.CautMeci(meciId);
			var comandaStartMeci = new ComandaStartMeci() { Meci = meciDto };
			MagistralaComenzi.Instanta.Value.Trimite(comandaStartMeci);
			return View("Index", GenereazaModelView(meciDto));
		}

		[HttpPost]
		public ActionResult IncrementareEchipa1(Guid meciId)
		{
			var meciDto = _readRepo.CautMeci(meciId);
			var comandaGolMarcat = new ComandaGolMarcat()
			{
				IdMeci = meciId,
				GolMarcat = new GolMarcatDto() { NumeEchipa = meciDto.Echipa1 }
			};
			MagistralaComenzi.Instanta.Value.Trimite(comandaGolMarcat);
			meciDto = _readRepo.CautMeci(meciId);
			return View("Index", GenereazaModelView(meciDto));
		}

		[HttpPost]
		public ActionResult IncrementareEchipa2(Guid meciId)
		{
			var meciDto = _readRepo.CautMeci(meciId);
			var comandaGolMarcat = new ComandaGolMarcat()
			{
				IdMeci = meciId,
				GolMarcat = new GolMarcatDto() { NumeEchipa = meciDto.Echipa2 }
			};
			MagistralaComenzi.Instanta.Value.Trimite(comandaGolMarcat);
			meciDto = _readRepo.CautMeci(meciId);
			return View("Index", GenereazaModelView(meciDto));
		}

		public ActionResult ProgramareMeci()
		{
			var meciNouDto = new MeciDto()
			{
				Id = Guid.NewGuid(),
				Data = DateTime.Now,
				Echipa1 = "Poli",
				Echipa2 = "FCSB"
			};
			_writeRepo.ProgrameazaMeci(meciNouDto);
			return View();
		}
	}
}