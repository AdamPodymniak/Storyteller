import { useRef, useState } from 'react';
import { useNavigate, useLocation, Link } from 'react-router-dom';
import axios from '../../api/axios';
import Cookies from 'universal-cookie';
import styles from './Login.module.css';

const cookies = new Cookies();


const Login = () => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const [errMsg, setErrMsg] = useState('');

    const loginRef = useRef(false);

    const navigate = new useNavigate();
    const location = useLocation();
    const from = location.state?.from?.pathname || "/";

    const handleSubmit = async (e)=>{
        e.preventDefault();
        try {
            if(!loginRef.current){
                const { data } = await axios.post('/Auth/login',
                    JSON.parse(JSON.stringify({username, password}))
                )
                cookies.set('refreshToken', data.refreshToken, { path: "/" });
                localStorage.setItem('jwtToken', data.jwtToken);
                localStorage.setItem('role', data.role);
                navigate(from, { replace: true });
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
        <div className="Container">
            <div className={styles.Login}>
                <form onSubmit={handleSubmit}>
                    <h2 className={styles.Title}>Login</h2>
                    
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
                    <button className={styles.button} type="submit">LOG IN</button>
                    <p className={styles.BottomLine}>
                        <Link className={styles.a} to="/register" replace>— Register —</Link>
                    </p>
                </form>
            </div>
        </div>
    )
}

export default Login