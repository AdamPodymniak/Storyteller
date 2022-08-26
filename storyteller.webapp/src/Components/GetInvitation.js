import { useRef, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';

import useAxiosPrivate from '../Hooks/useAxiosPrivate';


const GetInvitation = () => {

    const [invite, setInvite] = useState('');
    const [role, setRole] = useState('Reader');

    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const location = useLocation();
    const inviteRef = useRef(false);

    const eventHandler = async ()=>{
        if(!inviteRef.current){
            try{
                const body = JSON.parse(JSON.stringify({role}));
                const { data } = await axiosPrivate.post('/Auth/getinvitation', body);
        
                setInvite(data);
    
                return ()=>{
                    inviteRef.current = true;
                }
            }catch(err){
                navigate('/login', { state: { from: location }, replace: true });
            }
        }
    }

    return (
        <div>
            <p>GetInvitation</p>
            <select value={role} onChange={(e)=>setRole(e.target.value)}>
                <option value="Reader">Reader</option>
                <option value="Writer">Writer</option>
                <option value="Admin">Admin</option>
            </select>
            <button onClick={eventHandler}>Click</button>
            {invite ? <p>{invite}</p> : null}
        </div>
    )
}

export default GetInvitation