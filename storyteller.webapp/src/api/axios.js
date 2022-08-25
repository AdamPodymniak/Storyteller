import axios from 'axios'

const BASE_URL = "https://localhost:7144"

export default axios.create({
    baseURL: BASE_URL,
})