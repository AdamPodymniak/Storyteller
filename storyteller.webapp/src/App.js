import {
  Routes,
  Route
} from "react-router-dom";

import Register from './Components/Register'
import Login from './Components/Login'
import GetInvitation from "./Components/GetInvitation";
import Main from "./Components/Main";
import Layout from "./Components/Layout";
import RequireAuth from "./Components/RequireAuth";

const ROLES = {
  Admin: "4214564343",
  Writer: "8546342134",
  Reader: "0978441234"
}

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route path="/" element={<Main />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route element={<RequireAuth allowedRoles={[ROLES.Admin]} />}>
          <Route path="/getinvitation" element={<GetInvitation />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
