import { Link } from 'react-router-dom'
import styles from './Unauthorized.module.css';

const Unauthorized = () => {
  return (
    <div className="Container">
        <div className={styles.Unauthorized}>
          <h2>Unauthorized</h2>
          <p>You are not authorized to enter this page!</p><br />
          <Link to="/">Go back</Link>
        </div>
    </div>
  )
}

export default Unauthorized