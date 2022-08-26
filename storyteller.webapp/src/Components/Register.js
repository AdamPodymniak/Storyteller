import { useRef, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import axios from '../api/axios';
import './Login.css';

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
                navigate('/login');
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
        <div className="Container">
            <div className="Register">
                <form onSubmit={handleSubmit}>
                <h2 class="Title">Register</h2>
                {errMsg ? (<p>{errMsg}</p>) : (null)}
                    <input
                        placeholder='Username'
                        id="username"
                        name="username"
                        type="text"
                        autoComplete='off'
                        value={username}
                        onChange={(e)=>setUsername(e.target.value)}
                        required
                    /><br />
                    <input
                        placeholder='Password'
                        id="password"
                        name="password"
                        type="password"
                        value={password}
                        onChange={(e)=>setPassword(e.target.value)}
                        required
                    /><br />
                    <input
                        placeholder='Repeat Password'
                        id="repeatPassword"
                        name="repeatPassword"
                        type="password"
                        value={repeatPassword}
                        onChange={(e)=>setRepeatPassword(e.target.value)}
                        required
                    /><br />
                    <input
                        placeholder='Invitation'
                        autoComplete='off'
                        id="invitation"
                        name="invitation"
                        type="text"
                        value={invitation}
                        onChange={(e)=>setInvitation(e.target.value)}
                        required
                    /><br />
                    <button type="submit">Log In</button>
                    <p className="BottomLine">
                        <Link to="/login" replace>— Or Login —</Link>
                    </p>
                </form>
            </div>
        </div>
    )
}

export default Register