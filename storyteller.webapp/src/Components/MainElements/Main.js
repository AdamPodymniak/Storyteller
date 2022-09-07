import { useEffect, useState, useRef } from 'react';
import styles from './Main.module.css'
import CreateStory from './CreateStory'
import useAxiosPrivate from '../../Hooks/useAxiosPrivate'

const Main = () => {
    const isLoaded = useRef(false);
    const [data, setData] = useState();
    const axiosPrivate = useAxiosPrivate();
    useEffect(()=>{
        if(!isLoaded.current){
            (async function loadData(){
                const response = await axiosPrivate.get('/StoryEditor/get');
                setData(response.data);
                console.log(response.data);
            })()
            isLoaded.current = true;
        }
    }, [axiosPrivate, data])

    return(
        <div className={styles.Container}>
            {data?.map(item=>{
                return(
                    <div key={item.id}>
                        <h3>{item.name}</h3>
                        <p>{item.description}</p>
                        <img width="100px" src={"https://localhost:7144/" + item.imgPath} alt="idk"></img>
                    </div>
                )
            })}
            <CreateStory />
        </div>
    )
}

export default Main