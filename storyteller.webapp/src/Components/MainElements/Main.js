// import { useState } from 'react';
// import useAxiosPrivate from '../../Hooks/useAxiosPrivate'
import styles from './Main.module.css';

const Main = () => {
    
    // const [name, setName] = useState('');
    // const [description, setDescription] = useState('');
    // const [fileData, setFile] = useState('');

    // const axiosPrivate = useAxiosPrivate();

    // const userGuid = localStorage.getItem('guid');

    // const handleSubmit = async (e)=>{
    //     e.preventDefault();
    //     const file = new FormData();
    //     file.append("formFile", fileData);
    //     file.append("fileName", fileData.name);
    //     console.log(JSON.parse(JSON.stringify({name, description, file, userGuid})))
    //     const { data } = axiosPrivate.post('/StoryEditor/add', 
    //         JSON.parse(JSON.stringify({name, description, file, userGuid}))
    //     )
    //     console.log(data);
    // }

    return (
        <div className={styles.Container}>
            <p>Main</p>
            {/* <form onSubmit={handleSubmit}>
                    <input
                        className={styles.input}
                        placeholder='Name'
                        id="name"
                        name="name"
                        type="text"
                        value={name}
                        onChange={e=>setName(e.target.value)}
                        autoComplete='off'
                        required
                    /><br />
                    <textarea
                        className={styles.input}
                        placeholder='Description'
                        id="description"
                        name="description"
                        value={description}
                        onChange={e=>setDescription(e.target.value)}
                        required
                    /><br />
                    <input
                        className={styles.input}
                        placeholder='File'
                        id="file"
                        name="file"
                        type="file"
                        onChange={(e)=>setFile(e.target.files[0])}
                        required
                    /><br />
                    <button className={styles.AddButton} type="submit">CREATE</button>
                </form> */}
        </div>
    )
}

export default Main