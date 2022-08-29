import { Navigate, Outlet } from 'react-router-dom'

const RequireLogout = () => {
    const jwtToken = localStorage.getItem('jwtToken');
    return (
           !jwtToken ? <Outlet /> : 
           <Navigate to="/" replace />
    )
}

export default RequireLogout