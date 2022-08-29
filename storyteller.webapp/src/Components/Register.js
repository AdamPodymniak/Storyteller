import { useRef, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import axios from '../api/axios';
import styles from './Login.module.css';

const Register = () => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [repeatPassword, setRepeatPassword] = useState('');
    const [invitation, setInvitation] = useState('');

    const [errMsg, setErrMsg] = useState('');

    const registerRef = useRef(false);

    const navigate = useNavigate();

    const mediumRegex = new RegExp("^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})");

    const handleSubmit = async (e)=>{
        e.preventDefault();
        if(username.length < 3){
            setErrMsg("Username must be at least 3 characters long!");
            setPassword('');
            setRepeatPassword('');
            return ;
        }
        if(!mediumRegex.test(password)){
            setErrMsg("Insert better password!");
            setPassword('');
            setRepeatPassword('');
            return ;
        }
        if(password !== repeatPassword){
            setErrMsg("Passwords don't match!");
            setPassword('');
            setRepeatPassword('');
            return ;
        }
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
            if(err.response.data === "UsrErr"){
                setErrMsg("User already exists!");
            }
            else if(err.response.data === "InvErr"){
                setErrMsg("Wrong invitation!");
            }
            else{
                setErrMsg("Something went wrong");
            }
            setPassword('');
            setRepeatPassword('');
        }
    }

    return (
        <div className="Container">
            <div className={styles.Register}>
                <form onSubmit={handleSubmit}>
                <h2 className={styles.Title}>Register</h2>
                {errMsg ? (<p className={styles.Description}>{errMsg}</p>) :
                <p className={styles.Description}>Do you have an account?</p>}
                    <input
                        className={styles.input}
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
                        className={styles.input}
                        placeholder='Password'
                        id="password"
                        name="password"
                        type="password"
                        value={password}
                        onChange={(e)=>setPassword(e.target.value)}
                        required
                    /><br />
                    <input
                        className={styles.input}
                        placeholder='Repeat Password'
                        id="repeatPassword"
                        name="repeatPassword"
                        type="password"
                        value={repeatPassword}
                        onChange={(e)=>setRepeatPassword(e.target.value)}
                        required
                    /><br />
                    <input
                        className={styles.input}
                        placeholder='Invitation'
                        autoComplete='off'
                        id="invitation"
                        name="invitation"
                        type="text"
                        value={invitation}
                        onChange={(e)=>setInvitation(e.target.value)}
                        required
                    /><br />
                    <button className={styles.button} type="submit">REGISTER</button>
                    <p className={styles.BottomLine}>
                        <Link className={styles.a} to="/login" replace>— Login —</Link>
                    </p>
                </form>
            </div>
        </div>
    )
}

export default Register