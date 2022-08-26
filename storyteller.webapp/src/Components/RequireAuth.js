import { useLocation, Navigate, Outlet } from 'react-router-dom'

import React from 'react'

const RequireAuth = ({ allowedRoles }) => {
    const location = useLocation();
    const role = localStorage.getItem('role');
    const jwtToken = localStorage.getItem('jwtToken');
    return (
        allowedRoles.find(r => r === role) ?
        <Outlet /> :
           jwtToken ? <Navigate to="/unauthorized" state={{ from: location }} replace /> : 
           <Navigate to="/login" state={{ from: location }} replace />
    )
}

export default RequireAuth