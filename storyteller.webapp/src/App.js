import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link
} from "react-router-dom";

import Register from './Components/Register'
import Login from './Components/Login'
import GetInvitation from "./Components/GetInvitation";

function App() {
  return (
    <Router>
      <div>
      <nav className="navbar">
          <h2 className="logo">STORYTELLER</h2>
          <ul>
            <li>
              <Link to="/">\Login\</Link>
            </li>
            <li>
              <Link to="/register">\Register\</Link>
            </li>
            <li>
              <Link to="/getinvitation">\Invite\</Link>
            </li>
          </ul>
        </nav>
        <div className="App">
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/getinvitation" element={<GetInvitation />} />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;
