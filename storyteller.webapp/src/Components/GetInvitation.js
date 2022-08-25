import { useRef, useState } from 'react';
import Cookies from 'universal-cookie';

import axios from "../api/axios";

const cookies = new Cookies();

const GetInvitation = () => {

    const [invite, setInvite] = useState('');
    const [role, setRole] = useState('Reader');

    const inviteRef = useRef(false);

    const eventHandler = async ()=>{
        if(!inviteRef.current){
            const body = JSON.parse(JSON.stringify({role}));
            const { data } = await axios.post('/Auth/getinvitation', body, {
                headers: {
                    Authorization: `Bearer ${cookies.get('accessToken')}`
                }
            });
    
            setInvite(data);

            return ()=>{
                inviteRef.current = true;
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