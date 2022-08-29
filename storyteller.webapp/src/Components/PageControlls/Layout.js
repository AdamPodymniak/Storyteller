import { Outlet, useNavigate, useLocation } from 'react-router-dom'
import Cookies from 'universal-cookie';
import styles from './Layout.module.css';
import { FiLogOut } from 'react-icons/fi'
// import { FcInvite } from 'react-icons/fc'

const cookies = new Cookies();

const Layout = () => {

    const navigate = useNavigate();
    const location = useLocation();

    // const role = localStorage.getItem('role');

    const handleLogout = () =>{
        localStorage.removeItem('role');
        localStorage.removeItem('jwtToken');
        cookies.remove('refreshToken', { path: "/" });
        navigate('/login', { state: { from: location }, replace: true });
    }
  return (
    <main className="Container">
      <header className={styles.Header}>
        <h2 className={styles.Title}>STORYTELLER</h2>
        <nav className={styles.Navbar}>
          {/* {role === "4214564343" ?<Link to="/getinvitation">Invite</Link> : null} */}
          <FiLogOut onClick={() => handleLogout()} />
        </nav>
      </header>
      <Outlet />
    </main>
  )
}
export default Layout
