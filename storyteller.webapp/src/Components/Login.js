import { useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from '../api/axios';
import Cookies from 'universal-cookie';

const cookies = new Cookies();


const Login = () => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const [errMsg, setErrMsg] = useState('');

    const loginRef = useRef(false);

    const navigate = new useNavigate();

    const handleSubmit = async (e)=>{
        e.preventDefault();
        try {
            if(!loginRef.current){
                const { data } = await axios.post('/Auth/login',
                    JSON.parse(JSON.stringify({username, password}))
                )
                cookies.set('accessToken', data.jwtToken, { path: "/" });
                cookies.set('refreshToken', data.refreshToken, { path: "/" });
                console.log(data)
                navigate('/getinvitation');
                return () => {
                    loginRef.current = true;
                }
            }
        } catch(err){
            if(err){
                setErrMsg("Your username or password is wrong!");
                setPassword('');
            }
        }
    }

    return (
        <div style={{textAlign: "center"}}>
            <h2>Login</h2>
            {errMsg ? (<p>{errMsg}</p>) : (null)}
            <form onSubmit={handleSubmit}>
                <label htmlFor="username">Username: </label><br />
                <input
                    id="username"
                    name="username"
                    type="text"
                    autoComplete='off'
                    value={username}
                    onChange={(e)=>setUsername(e.target.value)}
                    required
                /><br />
                <label htmlFor="password">Password: </label><br />
                <input
                    id="password"
                    name="password"
                    type="password"
                    value={password}
                    onChange={(e)=>setPassword(e.target.value)}
                    required
                /><br />
                <button type="submit" style={{padding: "10px 30px", backgroundColor: "#0aa9ff"}}>Log In</button>
            </form>
        </div>
    )
}

export default Login