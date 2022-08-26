import axios from '../api/axios'
import Cookies from 'universal-cookie'

const cookies = new Cookies();

const useRefreshToken = () => {
  const refresh = async () =>{
    const jwtToken = localStorage.getItem('jwtToken');
    const refreshToken = cookies.get('refreshToken');
    const body = JSON.parse(JSON.stringify({jwtToken, refreshToken}))
    const { data } = await axios.post('Auth/refresh', 
        body
    )
    return data;
  }
  return refresh
}

export default useRefreshToken