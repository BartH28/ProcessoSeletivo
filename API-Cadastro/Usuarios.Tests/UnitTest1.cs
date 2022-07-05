using API_Cadastro.Controllers;
using API_Cadastro.Domain;
using API_Cadastro.Infra.Contexts;
using API_Cadastro.Interfaces;
using API_Cadastro.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Usuarios.Tests;

public class UnitTest1
{
    [Fact]
    public void Deve_Criar_Usuario()
    {
        var fakerepo = new Mock<IUsuarioRepository>();

        var fakeLogger = new Mock<ILogger<UsuariosController>>();

        //fakeLogger.Setup(l => l.CreateLogger("Usuario"));
        ILogger<UsuariosController> logger = fakeLogger.Object ;

        logger = Mock.Of<ILogger<UsuariosController>>();

        fakerepo.Setup(x => x.Criar(It.IsAny<Usuario>())).Returns((Usuario) null);

        Usuario usuario = new Usuario();

        usuario.FirstName = "teste";
        usuario.Surname = "Test";
        usuario.Age = 10;
       

        

        var controller = new UsuariosController(fakerepo.Object, logger);

        var CriarUsuarioFake = controller.Cadastrar(usuario);


        Assert.IsType<CreatedResult>(CriarUsuarioFake);

    }


    [Fact]
    public void Deve_Retornar_Um_Usuario()
    {
        var fakerepo = new Mock<IUsuarioRepository>();

        var fakeLogger = new Mock<ILogger<UsuariosController>>();

        ILogger<UsuariosController> logger = fakeLogger.Object;

        logger = Mock.Of<ILogger<UsuariosController>>();

        fakerepo.Setup(x => x.BuscarUsuario(It.IsAny<int>())).Returns((Usuario)null);

        var fakeid = 2;
        var controller = new UsuariosController(fakerepo.Object, logger);

        var BuscarUsuarioFake = controller.BuscarPorId(fakeid);

        bool resultado = false;

        if (BuscarUsuarioFake != null)
        {
            resultado = true;
        }

        Assert.True(resultado);
    }

    [Fact]
    public void Deve_Retornar_Lista_Usuarios()
    {
        var fakerepo = new Mock<IUsuarioRepository>();

        var fakeLogger = new Mock<ILogger<UsuariosController>>();

        ILogger<UsuariosController> logger = fakeLogger.Object;

        logger = Mock.Of<ILogger<UsuariosController>>();

        fakerepo.Setup(x => x.Listar()).Returns((IEnumerable<Usuario>)null);

        var controller = new UsuariosController(fakerepo.Object, logger);

        var ListarUsuariosFake = controller.Listar();

        bool resultado = false;

        if (ListarUsuariosFake != null)
        {
            resultado = true;
        }

        Assert.True(resultado);
    }
    [Fact]
    public void Deve_Deletar_Um_Usuario()
    {
        var fakerepo = new Mock<IUsuarioRepository>();

        var fakeLogger = new Mock<ILogger<UsuariosController>>();

        ILogger<UsuariosController> logger = fakeLogger.Object;

        logger = Mock.Of<ILogger<UsuariosController>>();

        fakerepo.Setup(x => x.Deletar(It.IsAny<int>()));

        var controller = new UsuariosController(fakerepo.Object, logger);

        var fakeid = 2;

        var DeletarUsuarioFake = controller.Deletar(fakeid);

        bool resultado = false;

        if (DeletarUsuarioFake != null)
        {
            resultado = true;
        }

        Assert.True(resultado);
    }

    [Fact]
    public void Deve_Alterar_Um_Usuario()
    {
        var fakerepo = new Mock<IUsuarioRepository>();

        var fakeLogger = new Mock<ILogger<UsuariosController>>();

        ILogger<UsuariosController> logger = fakeLogger.Object;

        logger = Mock.Of<ILogger<UsuariosController>>();

        fakerepo.Setup(x => x.Atualizar(It.IsAny<int>(),It.IsAny<Usuario>())).Returns((Usuario) null);

        var controller = new UsuariosController(fakerepo.Object, logger);

        var fakeid = 2;

        var mockUserAtt = new Mock<Usuario>();

        Usuario fakeUserAtt = mockUserAtt.Object;
        

        var AlterarUsuarioFake = controller.Alterar(fakeid, fakeUserAtt);

        bool resultado = false;

        if (AlterarUsuarioFake != null)
        {
            resultado = true;
        }

        Assert.True(resultado);
    }




}
