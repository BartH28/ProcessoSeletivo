import "../Assets/Css/ManagementPage.css"
import React, { useState, useEffect } from 'react'
import { useNavigate } from "react-router-dom";
import axios from 'axios'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { parseJwt } from "../Utils/auth";


export const ManagementPage = () => {
    const [Usuario, setUsuario] = useState({ nome: "", email: "", senha: "", status: true, idTipoUsuario: 1 })
    const [EventUser, setEventUser] = useState(false)
    const [ListaUsuarios, setListaUsuarios] = useState([])
    const [senha, setSenha] = useState("")
    const [id, setId] = useState("")
    const [status, setStatus] = useState(true)

    const nav = useNavigate()

    function listarUsuarios() {
        axios('http://localhost:5000/api/Usuarios/')
            .then(r => {
                setListaUsuarios(r.data)

            }).catch(err => console.log(err))
    }

    function AlterUsuario(id) {
        setEventUser(true)
        console.log(id)
        setId(id)
        axios('http://localhost:5000/api/Usuarios/' + id)
            .then(r => {
                setUsuario(r.data)
            }).catch(err => console.log(err))
    }



    function CadastrarUsuario(event) {
        event.preventDefault()
        Usuario.senha = senha
        Usuario.status = status
        console.log(Usuario)

        if (EventUser === false) {
            axios.post('http://localhost:5000/api/Usuarios/', Usuario, {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('user-token')
                }
            })
                .then(r => {
                    console.log(r)
                    toast.success('Usuario ' + Usuario.nome + ' criado')
                    setUsuario({})

                    listarUsuarios()
                }).catch(err => console.log(err))

            return
        }

        if (EventUser === true) {
            axios.put('http://localhost:5000/api/Usuarios/' + id, Usuario, {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('user-token')
                }
            })
                .then(r => {
                    console.log(r)
                    // setUsuario({})
                    toast.success('Usuario alterado com sucesso')
                    listarUsuarios()
                }).catch(err => {
                    toast.error('Erro ao efetuar alteração')
                    console.log(err)
                })

            return
        }
    }

    function handlerEvents(event) {
        console.log(EventUser)

        setEventUser(false)
        setUsuario({ nome: "", email: "", senha: "", status: true, idTipoUsuario: 1 })
        setSenha("")
    }

    function DeleteUser(id) {

        axios.delete('http://localhost:5000/api/Usuarios/' + id, {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('user-token')
            }
        })
            .then(r => {
                console.log(r)
                toast.info("Usuario deletado com sucesso!")
                listarUsuarios()
            }).catch(err => { console.log(err) })
    }

    function Logout() {
        localStorage.clear()
        nav("/")
    }

    useEffect(() => {
        listarUsuarios()
    }, [])


    return (
        <main className='LoginMain'>
            <ToastContainer />
            <div className="Align">
                <div className={parseJwt().role === '3'? "ContainerGetBack":"ContainerGetBackA" }>
                    <button onClick={() => nav("/profile")} className="btn-header alterar">Meu Perfil</button>
                    <button onClick={() => Logout()} className="btn-header logout" style={{marginLeft: '1rem'}}>Logout</button>
                </div>

                <div className="Container-Manage">
                    <div className='Management-Container'>
                        <table class="styled-table">
                            <thead>
                                <tr>
                                    <th>Nome</th>
                                    <th>Email</th>
                                    <th>Tipo</th>
                                    <th>Status</th>
                                    <th></th>
                                    {parseJwt().role === '3' && <th></th>}
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    ListaUsuarios.map((u) => {
                                        return (
                                            <tr key={u.idUsuario} class="active-row">
                                                <td>{u.nome}</td>
                                                <td>{u.email}</td>
                                                <td>{u.idTipoUsuarioNavigation.nomeTipo}</td>
                                                <td>{u.status ? "Ativo" : "Inativo"}</td>
                                                <td><button className="btnM alterar" onClick={() => AlterUsuario(u.idUsuario)}>Alterar</button></td>
                                                {parseJwt().role === '3' && <td><button className="btnM logout" onClick={() => DeleteUser(u.idUsuario)}>Deletar</button></td>}
                                            </tr>
                                        )
                                    })
                                }

                                {/* <tr  class="active-row">
                                <td>a</td>
                                <td>a</td>
                                <td>a</td>
                                <td>a</td>
                                <td><button>a</button></td>
                            </tr> */}
                            </tbody>
                        </table>

                    </div>
                    <div className='Container-Cadastro'>
                        <form onSubmit={CadastrarUsuario} className='Profile-Container'>
                            <div className='flex-center'>
                            </div>

                            {
                                EventUser === true && <button className="Back-Btn" onClick={() => handlerEvents()}>Criar novo Usuario</button>
                            }

                            <span className="Subtitle-Forms">{EventUser ? 'Alterar' : 'Criar'}</span>
                            <div className='Container-Inputs'>
                                <label className='Label-Form'>Nome</label>
                                <input type="text" className='input-Form' value={Usuario.nome} onChange={u => setUsuario(prevState => ({
                                    ...prevState,
                                    nome: u.target.value
                                }))} />
                            </div>
                            <div className='Container-Inputs'>
                                <label className='Label-Form'>Email</label>
                                <input type="email" className='input-Form' value={Usuario.email} onChange={u => setUsuario(prevState => ({
                                    ...prevState,
                                    email: u.target.value
                                }))} />
                            </div>
                            <div className='Container-Inputs'>
                                <label className='Label-Form'>Status</label>
                                {/* <span>{status? "Ativo":"Inativo"}</span> */}
                                {/* <input type="checkbox" value={status} onChange={s => setStatus(s.target.checked)}/> */}
                                <select className='input-Form' value={status} onChange={u => setStatus(u.target.value)} >
                                    <option value={true}>Ativo</option>
                                    <option value={false}>Inativo</option>
                                </select>
                            </div>
                            {
                                EventUser === false && <div className='Container-Inputs'><label className='Label-Form'>Tipo</label><select className='input-Form' value={Usuario.idTipoUsuario} onChange={u => setUsuario(prevState => ({ ...prevState, idTipoUsuario: u.target.value }))} ><option value={1}>Geral</option><option value={2}>Admin</option><option value={3}>Root</option></select></div>
                            }

                            <div className='Container-Inputs'>
                                <label className='Label-Form'>Senha</label>
                                <input type="text" className='input-Form' value={senha} onChange={s => setSenha(s.target.value)} />
                            </div>
                            <button type="submit" className='btn-Login'>{EventUser ? "Alterar" : "Criar"}</button>
                        </form>
                    </div>
                </div>
            </div>
        </main>
    )
}
