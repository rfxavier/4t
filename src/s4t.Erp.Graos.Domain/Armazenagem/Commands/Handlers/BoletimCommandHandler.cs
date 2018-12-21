using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Core.Domain.Commands;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Armazenagem.Commands.Inputs;
using s4t.Erp.Graos.Domain.Armazenagem.Commands.Results;
using s4t.Erp.Graos.Domain.Armazenagem.Dtos;
using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.Interfaces;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Domain.Core.CommandHandler;
using s4t.Erp.Graos.Domain.Core.Interfaces;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Commands.Handlers
{
    public class BoletimCommandHandler : CommandHandler, ICommandHandler<RegistraBoletimDocumentoEntradaCommand>
    {
        private readonly IFilialRepository _filialRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDocumentoEntradaRepository _documentoEntradaRepository;
        private readonly IInstrucaoServicoRepository _instrucaoServicoRepository;
        private readonly ITransferenciaRepository _transferenciaRepository;
        private readonly IOrdemCarregamentoRepository _ordemCarregamentoRepository;
        private readonly IArmazemRepository _armazemRepository;
        private readonly ILocalRepository _localRepository;
        private readonly IPilhaRepository _pilhaRepository;
        private readonly IBoletimRepository _boletimRepository;

        public BoletimCommandHandler(
            IFilialRepository filialRepository,
            IUsuarioRepository usuarioRepository,
            IDocumentoEntradaRepository documentoEntradaRepository,
            IInstrucaoServicoRepository instrucaoServicoRepository,
            ITransferenciaRepository transferenciaRepository,
            IOrdemCarregamentoRepository ordemCarregamentoRepository,
            IArmazemRepository armazemRepository,
            ILocalRepository localRepository,
            IPilhaRepository pilhaRepository,
            IBoletimRepository boletimRepository,
            IUnitOfWork uow, IDomainNotificationHandler<DomainNotification> notifications) : base(uow, notifications)
        {
            _filialRepository = filialRepository;
            _usuarioRepository = usuarioRepository;
            _documentoEntradaRepository = documentoEntradaRepository;
            _instrucaoServicoRepository = instrucaoServicoRepository;
            _transferenciaRepository = transferenciaRepository;
            _ordemCarregamentoRepository = ordemCarregamentoRepository;
            _armazemRepository = armazemRepository;
            _localRepository = localRepository;
            _pilhaRepository = pilhaRepository;
            _boletimRepository = boletimRepository;
        }

        public ICommandResult Handle(RegistraBoletimDocumentoEntradaCommand command)
        {
            //Validações de coisas que não vão em repositório
            if (!(command.PossuiNumeroInformado()
                  & command.PossuiDataInformada()
                  & command.PossuiItemInformado()
                  & command.PossuiBoletimDocumentoInformado()
                  & command.PossuiLoteInformado()
                  & command.PossuiSacasMaiorQueZero()
                  & command.PossuiLocalizacaoOrigemInformada()
                  & command.PossuiLocalizacaoDestinoInformada()))
            {
                return new RegistraBoletimCommandResult();
            }

            //Validações de coisas que vão em repositório - parte 1
            var filial = _filialRepository.ObterPorId(command.FilialId);
            var usuario = _usuarioRepository.ObterPorId(command.UsuarioId);

            if (!(command.PossuiFilialInformada(filial)
                  & command.PossuiUsuarioInformado(usuario)))
            {
                return new RegistraBoletimCommandResult();
            }

            //Validações de coisas que vão em repositório - parte 2

            //Gera os Value Objects
            //BoletimDocumento
            //todo tests handler: boletimDocumentoDto obrigatório ter Serie = command.BoletimDocumentoSerie (construtor do command já encaixou essa Série = Nde)
            var boletimDocumentoDto = new BoletimDocumentoDto()
            {
                FilialId = filial.Id,
                Serie = command.BoletimDocumentoSerie,
                Numero = command.BoletimDocumentoNumero
            };

            var boletimDocumento = boletimDocumentoDto.ObterBoletimDocumento(
                _documentoEntradaRepository,
                _instrucaoServicoRepository,
                _transferenciaRepository,
                _ordemCarregamentoRepository);

            //Localização Origem
            var localizacaoOrigemDto = new LocalizacaoDto()
            {
                EmpresaId = filial.EmpresaId,
                FilialCodigo = command.OrigemFilialCodigo,
                ArmazemCodigo = command.OrigemArmazemCodigo,
                Quadra = command.OrigemQuadra,
                Bloco = command.OrigemBloco
            };

            var localizacaoOrigem = localizacaoOrigemDto.ObterLocalizacao(_filialRepository, _armazemRepository, _localRepository, _pilhaRepository);

            //Localização Destino
            var localizacaoDestinoDto = new LocalizacaoDto()
            {
                EmpresaId = filial.EmpresaId,
                FilialCodigo = command.DestinoFilialCodigo,
                ArmazemCodigo = command.DestinoArmazemCodigo,
                Quadra = command.DestinoQuadra,
                Bloco = command.DestinoBloco
            };

            var localizacaoDestino = localizacaoDestinoDto.ObterLocalizacao(_filialRepository, _armazemRepository, _localRepository, _pilhaRepository);

            if (!(localizacaoOrigem.EstaValidaECadastrada()
                  & localizacaoDestino.EstaValidaECadastrada()))
            {
                return new RegistraBoletimCommandResult();
            }

            if (AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(localizacaoOrigem != localizacaoDestino, "Localizações de origem e destino devem ser diferentes")))
            {
                return new RegistraBoletimCommandResult();
            }

            var lote = command.ObterLote(boletimDocumento);

            if (lote == null)
            {
                return new RegistraBoletimCommandResult();
            }

            lote.AlteraLocalizacao(localizacaoDestino);

            //todo 03-03-18: Atualizar endereço destino por fora no handler --OK, mas TESTAR+
            //todo 03-03-18: Abordar tratamento de duplicidade de Lote pelo número + filial Id (TRATAR ESSA CONSISTÊNCIA COM MODELAGEM DE BoletimPermissao)

            //todo 06-03-18: integration tests boletimRepository.Add (verificar se lote passado irá se comportar como 1) novo lote inserido, ou 2) lote existente atualizando localização endereço

            //Gera nova entidade
            var boletim = new Boletim(Guid.NewGuid(), filial.Id, usuario.Id, command.Numero, command.Data, command.Item,
                TipoGrao.FromValue(boletimDocumento.DocumentoEntrada.NotaFiscalGraos.TipoGrao),
                boletimDocumento, lote, command.Sacas, localizacaoOrigem, localizacaoDestino, Guid.Empty, command.LoteUltimoSacas, String.Empty,
                string.Empty);


            //Adiciona as entidades ao repositório
            _boletimRepository.Add(boletim);

            return new RegistraBoletimCommandResult();
        }
    }
}