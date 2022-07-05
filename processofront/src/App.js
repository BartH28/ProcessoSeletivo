import './App.css';
import { BrowserRouter as Router, Routes, Route} from "react-router-dom";
import LoginPage from './Pages/LoginPage';
import { ProfilePage } from './Pages/ProfilePage';
import { ManagementPage } from './Pages/ManagementPage';


function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage/>}/>
        <Route path="/Profile" element={<ProfilePage/>}/>
        <Route path="/Management" element={<ManagementPage/>}/>
      </Routes>
    </Router>
  );
}

export default App;
