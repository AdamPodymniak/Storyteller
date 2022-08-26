import { Outlet, useNavigate, useLocation } from 'react-router-dom'
import Cookies from 'universal-cookie'

const cookies = new Cookies();

const Layout = () => {

    const navigate = useNavigate();
    const location = useLocation();

    const handleLogout = () =>{
        localStorage.removeItem('role');
        localStorage.removeItem('jwtToken');
        cookies.remove('refreshToken', { path: "/" });
        navigate('/login', { state: { from: location }, replace: true });
    }
  return (
    <main className="App">
        <button onClick={handleLogout}>Logout</button>
        <Outlet />
    </main>
  )
}
export default Layout
