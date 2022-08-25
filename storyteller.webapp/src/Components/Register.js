import { useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from '../api/axios';

const Register = () => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [repeatPassword, setRepeatPassword] = useState('');
    const [invitation, setInvitation] = useState('');

    const [errMsg, setErrMsg] = useState('');

    const registerRef = useRef(false);

    const navigate = useNavigate();

    const handleSubmit = async (e)=>{
        e.preventDefault();
        try {
            if(!registerRef.current){
                await axios.post('/Auth/register',
                    JSON.parse(JSON.stringify({username, password, repeatPassword, invitation}))
                )
                navigate('/');
                return () => {
                    registerRef.current = true;
                }
            }
        } catch(err){
            setErrMsg('');
            console.log(err.response.data)
                if(err.response.data === "PasErr1"){
                    setErrMsg("Password is too short!");
                }else if(err.response.data === "PasErr2"){
                    setErrMsg("Password don't match!");
                }
                else if(err.response.data === "UsrErr"){
                    setErrMsg("User already exists!");
                }
                else if(err.response.data === "InvErr"){
                    setErrMsg("Wrong invitation!");
                }
            }
        }

    return (
        <div style={{textAlign: "center"}}>
            <h2>Register</h2>
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
                <label htmlFor="repeatPassword">Repeat password: </label><br />
                <input
                    id="repeatPassword"
                    name="repeatPassword"
                    type="password"
                    value={repeatPassword}
                    onChange={(e)=>setRepeatPassword(e.target.value)}
                    required
                /><br />
                <label htmlFor="invitation">Invitation: </label><br />
                <input
                    id="invitation"
                    name="invitation"
                    type="text"
                    value={invitation}
                    onChange={(e)=>setInvitation(e.target.value)}
                    required
                /><br />
                <button type="submit" style={{padding: "10px 30px", backgroundColor: "#0aa9ff"}}>Log In</button>
            </form>
        </div>
    )
}

export default Register