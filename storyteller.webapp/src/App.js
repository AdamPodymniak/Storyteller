import {
  Routes,
  Route
} from "react-router-dom";

import Register from './Components/Auth/Register'
import Login from './Components/Auth/Login'
import GetInvitation from "./Components/Auth/GetInvitation";
import Main from "./Components/Main";
import Layout from "./Components/PageControlls/Layout";
import RequireAuth from "./Components/PageControlls/RequireAuth";
import RequireLogout from "./Components/PageControlls/RequireLogout";
import Unauthorized from "./Components/PageControlls/Unauthorized";
import PageNotFound from "./Components/PageControlls/PageNotFound";

const ROLES = {
  Admin: "4214564343",
  Writer: "8546342134",
  Reader: "0978441234"
}

function App() {
  return (
    <Routes>
      <Route element={<RequireLogout />}>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Route>
      <Route path="/" element={<Layout />}>
        <Route element={<RequireAuth allowedRoles={[ROLES.Admin, ROLES.Reader, ROLES.Writer]} />}>
          <Route path="/" element={<Main />} />
        </Route>
        <Route element={<RequireAuth allowedRoles={[ROLES.Admin]} />}>
          <Route path="/getinvitation" element={<GetInvitation />} />
        </Route>
      </Route>
      <Route path="/unauthorized" element={<Unauthorized />} />
      <Route path="*" element={<PageNotFound />} />
    </Routes>
  );
}

export default App;
