import axios from 'axios'
import React, {useState} from 'react'
import {useNavigate} from "react-router-dom";
import "../Assets/Css/LoginPage.css"
import logo from "../Assets/Img/Logo.png"
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


export default function LoginPage() {
  const [Usuario, setUsuario] = useState({email: "", senha:""})
  const nav = useNavigate()

  function Login(event) {
      event.preventDefault()
      axios.post('http://localhost:5000/api/Login', Usuario)
      .then( r => {
        localStorage.setItem('user-token', r.data.token)
        console.log(r)
        nav('/Profile')
      }).catch(err => {
        console.log(err) 
        toast.error("Login não efetuado corretamente!")
      })
      
      
  }

  return (
    <main className='LoginMain'>
      <div className=' Container'>
        <form onSubmit={Login} className='Forms-Container'>
          <div className='flex-center'>
            <img src={logo} alt="Logo 2rp" />
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
            <input type="password" value={Usuario.senha} className='input-Form'
              onChange={u => setUsuario(prevState => ({...prevState, senha: u.target.value }))}
            />
          </div>
          <button type='submit' className='btn-Login'>Login</button>
        </form>
      </div>
      <ToastContainer/>
    </main>
  )
}
