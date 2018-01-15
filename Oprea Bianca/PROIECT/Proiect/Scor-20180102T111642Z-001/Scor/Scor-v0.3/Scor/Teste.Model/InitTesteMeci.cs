using Moq;
using Scor.Evenimente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Model
{
	public class InitTesteMeci: IDisposable
	{
		public Mock<ProcesatorEveniment> MockProcesatorProgramareMeci { get; private set; }

		public InitTesteMeci()
		{
			MagistralaEvenimente.Instanta.Value.InregistreazaProcesatoareStandard();
			MockProcesatorProgramareMeci = new Mock<ProcesatorEveniment>();
			MagistralaEvenimente.Instanta.Value.InregistreazaProcesator(TipEveniment.ProgramareMeci, MockProcesatorProgramareMeci.Object);
			MagistralaEvenimente.Instanta.Value.InchideInregistrarea();
		}

		public void Dispose()
		{
			
		}
	}
}
