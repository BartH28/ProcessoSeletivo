import React, { useState, useEffect } from 'react'
import axios from 'axios'
import "../Assets/Css/ProfilePage.css"
import { parseJwt } from '../Utils/auth'
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export const ProfilePage = () => {
    const [Usuario, setUsuario] = useState({})
    const [TipoUsuario, setTU] = useState("")
    const [senha, setSenha] = useState("")
    const nav = useNavigate()
    function listarUsuario() {
        let id = parseJwt().jti
        axios('http://localhost:5000/api/Usuarios/' + id)
            .then(r => {
                setUsuario(r.data)
                setTU(r.data.idTipoUsuarioNavigation.nomeTipo)
                console.log(r)
            }).catch(err => console.log(err))
    }

    function Logout() {
        localStorage.clear()
        nav("/")
    }

    function AlterarUsuario(event) {
        event.preventDefault()
        Usuario.senha = senha;
        console.log(Usuario)
        axios.put("http://localhost:5000/api/Usuarios/"+ parseJwt().jti, Usuario, {
            headers: {
                Authorization: 'Bearer ' +  localStorage.getItem('user-token')
            }
         })
        .then(r => {
            console.log(r)
            toast.success("Sua conta foi altera!")
        }).catch(err => {
            console.log(err)
            toast.error("Sua conta não foi alterada corretamente")
        })
    }

    useEffect(() => {
        listarUsuario()
        console.log(parseJwt())
    }, [])

    return (
        <>
            <main className='LoginMain'>
                <ToastContainer/>
                <div className='Container-Profiles'>
                    <div className='Profile-Container'>
                        <div className='Info-Container'>
                            <h1 className='Title-Profile'>Profile</h1>
                            <div className="Details-Container">
                                <span>Nome: <span className='Varible-Info'>{Usuario.nome}</span></span>
                                <span>Email: <span className='Varible-Info'>{Usuario.email}</span></span>
                                <span>Usuario <span className='Varible-Info'>{TipoUsuario}</span></span>
                                <span>Status: <span className='Varible-Info'>{Usuario.status ? "Ativo" : "Inativo"}</span></span>
                            </div>
                                {
                                    parseJwt().role === '2' && <button className='btn alterar' onClick={() => nav('/Management')}>Gerenciamento</button>
                                }
                                {
                                    parseJwt().role === '3'&& <button className='btn alterar' onClick={() => nav('/Management')}>Gerenciamento</button>
                                }
                            <button className='btn logout' onClick={Logout}>Logout</button>
                        </div>
                    </div>
                </div>
                <div className='Container-Profiles'>
                    <form onSubmit={AlterarUsuario} className='Profile-Container'>
                        <div className='flex-center'>
                        </div>
                        <div className='Container-Inputs'>
                            <label className='Label-Form'>Nome</label>
                            <input type="text" value={Usuario.nome} className='input-Form' onChange={u => setUsuario(prevState => ({
                                ...prevState,
                                nome: u.target.value
                            }))} />
                        </div>
                        <div className='Container-Inputs'>
                            <label className='Label-Form'>Email</label>
                            <input type="email" value={Usuario.email} className='input-Form' onChange={u => setUsuario(prevState => ({
                                ...prevState,
                                email: u.target.value
                            }))} />
                        </div>
                        <div className='Container-Inputs'>
                            <label className='Label-Form'>Senha</label>
                            <input type="text" value={senha} className='input-Form'
                                onChange={u => setSenha(u.target.value)}
                            />
                        </div>
                        <button type='submit' className='btn-Login'>Alterar</button>
                    </form>
                </div>
            </main>

        </>
    )
}
