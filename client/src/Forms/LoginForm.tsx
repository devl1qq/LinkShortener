import React, { useState } from "react";
import axios from "axios";
import { Container, Typography, TextField, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

const LoginForm: React.FC = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const response = await axios.post("https://localhost:7215/api/User/login", {
        username,
        password,
      });
      const { token } = response.data;
      localStorage.setItem("token", token);
      navigate("/shortened-urls"); // Redirect to the ShortenedUrlsForm
    } catch (error) {
      setError("Invalid username or password");
    }
  };

  return (
    <Container maxWidth="xs" sx={{ mt: 4 }}>
      <Typography variant="h5" align="center" sx={{ mb: 2 }}>
        Login
      </Typography>
      <TextField
        label="Username"
        fullWidth
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        sx={{ mb: 2 }}
      />
      <TextField
        label="Password"
        fullWidth
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        sx={{ mb: 2 }}
      />
      {error && (
        <Typography variant="body1" color="error" align="center" sx={{ mb: 2 }}>
          {error}
        </Typography>
      )}
      <Button variant="contained" color="primary" fullWidth onClick={handleLogin}>
        Login
      </Button>
    </Container>
  );
};

export default LoginForm;
