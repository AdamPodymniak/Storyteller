import { useRef } from 'react';
import Cookies from 'universal-cookie';

import axios from "../api/axios";

const cookies = new Cookies();

const GetInvitation = () => {

    const inviteRef = useRef(false);

    const eventHandler = ()=>{
        if(!inviteRef.current){
            console.log(JSON.parse(JSON.stringify({"role": "Writer"})));
            const body = JSON.parse(JSON.stringify({"role": "Writer"}));
            const { data } = axios.post('/Auth/getinvitation', body, {
                headers: {
                    Authorization: `Bearer ${cookies.get('token')}`
                }
            });
    
            console.log(data);

            return ()=>{
                inviteRef.current = true;
            }
        }
    }

    return (
        <div>
            GetInvitation
            <button onClick={eventHandler}>Click</button>
        </div>
    )
}

export default GetInvitation