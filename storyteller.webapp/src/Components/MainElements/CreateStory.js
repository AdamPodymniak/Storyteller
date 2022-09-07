import { useState } from 'react';
import styles from './Main.module.css'
import useAxiosPrivate from '../../Hooks/useAxiosPrivate'

const CreateStory = () => {
  
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [file, setFile] = useState('');

    const axiosPrivate = useAxiosPrivate();

    const userGuid = localStorage.getItem('guid');

    const handleSubmit = async (e)=>{
        e.preventDefault();
        try{
            const formData = new FormData();
            formData.append("Name", name);
            formData.append("Description", description);
            formData.append("File", file);
            formData.append("UserGuid", userGuid);
            await axiosPrivate.post('/StoryEditor/add', formData);
            setName('');
            setDescription('');
        }catch(err){
            console.error(err);
        }
    }

    return (
            <form className={styles.CreateStory} onSubmit={handleSubmit}>
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
                        accept="image/png, image/gif, image/jpeg"
                        onChange={(e)=>setFile(e.target.files[0])}
                        required
                    /><br />
                    <button className={styles.AddButton} type="submit">CREATE</button>
                </form>
    )
}

export default CreateStory