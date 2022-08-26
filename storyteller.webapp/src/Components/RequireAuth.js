import { useLocation, Navigate, Outlet } from 'react-router-dom'

import React from 'react'

const RequireAuth = ({ allowedRoles }) => {
    const location = useLocation();
    const role = localStorage.getItem('role');
    return (
        allowedRoles.find(r => r===role) ?
        <Outlet /> :
        <Navigate to="/unauthorized" state={{ from: location }} replace />
    )
}

export default RequireAuth