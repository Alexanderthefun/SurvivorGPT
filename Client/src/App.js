import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router } from "react-router-dom";
import { Spinner } from 'reactstrap';
import Header from "./Components/Header/Header";
import ApplicationViews from './Components/ApplicationView';
import { AuthManager, getUserDetails, me, onLoginStatusChange } from "./Modules/authManager";
import firebase from 'firebase';
import Login from './Components/Auth/Login';
import  register  from './Components/Auth/Register';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(null)
  const [userProfile, setUserProfile] = useState(null)
  const [role, setRole] = useState("")

  useEffect(() => {
    onLoginStatusChange(setIsLoggedIn);
  }, []);

  useEffect(() => {
    if (isLoggedIn) {
      me().then(setUserProfile);
    } else {
      setUserProfile(null);
    }
  }, [isLoggedIn]);

  if (isLoggedIn === null) {
    return <Spinner className="app-spinner dark" />;
  }

  return (
    <Router>
      <Header isLoggedIn={isLoggedIn} role={role} />
      <ApplicationViews isLoggedIn={isLoggedIn} role={role} />
    </Router>
  );
}

export default App;
