import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { Container } from "@mui/material";
import RegistrationForm from "./Forms/RegistrationForm";
import LoginForm from "./Forms/LoginForm";
import MainPageForm from "./Forms/MainPageForm";
import ShortenedUrlsForm from "./Forms/ShortenedUrlsForm";
import DetailedInfoForm from "./Forms/DetailedInfoForm";

const App: React.FC = () => {
  return (
    <Router>
      <div>        
        <Container sx={{ mt: 2 }}>
          <Routes>
            <Route path="/" element={<MainPageForm />} />
            <Route path="/register" element={<RegistrationForm />} />
            <Route path="/login" element={<LoginForm />} />
            <Route path="/shortened-urls" element={<ShortenedUrlsForm />} />
            <Route path="/details/:id" element={<DetailedInfoForm />} />
          </Routes>
        </Container>
      </div>
    </Router>
  );
};

export default App;
