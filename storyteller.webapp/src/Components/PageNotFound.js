import { Link } from 'react-router-dom';
import styles from './PageNotFound.module.css';

const PageNotFound = () => {
  return (
    <div className="Container">
        <div className={styles.PageNotFound}>
            <h2>Page Not Found</h2>
            <p>This page resulted in error 404</p><br />
            <Link to="/">Go back</Link>
        </div>
    </div>
  )
}

export default PageNotFound