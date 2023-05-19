import React, { useEffect, useState } from "react";
import axios from "axios";
import { Container, Typography, Box, CircularProgress } from "@mui/material";
import { useParams } from "react-router-dom";

interface ShortenedUrl {
  id: number;
  shortUrl: string;
  originalUrl: string;
  createDate: string;
  createdBy: string;
}

const DetailedInfoForm: React.FC = () => {
  const [url, setUrl] = useState<ShortenedUrl | null>(null);
  const { id } = useParams<{ id: string }>();

  useEffect(() => {
    const fetchUrl = async () => {
      try {
        const token = localStorage.getItem("token");
        const response = await axios.get<ShortenedUrl>(
          `https://localhost:7215/api/Url/get_url_info/${id}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        setUrl(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchUrl();
  }, [id]);

  if (!url) {
    return (
      <Container maxWidth="md" sx={{ mt: 4 }}>
        <Box sx={{ display: "flex", justifyContent: "center", alignItems: "center", height: "60vh" }}>
          <CircularProgress />
        </Box>
      </Container>
    );
  }

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Box sx={{ textAlign: "center", my: 2 }}>
        <Typography variant="h5" sx={{ fontWeight: "bold" }}>
          Detailed Information
        </Typography>
      </Box>
      <Box sx={{ my: 2 }}>
        <Typography variant="body1" sx={{ fontWeight: "bold" }}>
          Shortened URL:
        </Typography>
        <Typography variant="body1">{url.shortUrl}</Typography>
      </Box>
      <Box sx={{ my: 2 }}>
        <Typography variant="body1" sx={{ fontWeight: "bold" }}>
          Original URL:
        </Typography>
        <Typography variant="body1">{url.originalUrl}</Typography>
      </Box>
      <Box sx={{ my: 2 }}>
        <Typography variant="body1" sx={{ fontWeight: "bold" }}>
          Created Date:
        </Typography>
        <Typography variant="body1">{url.createDate}</Typography>
      </Box>
      <Box sx={{ my: 2 }}>
        <Typography variant="body1" sx={{ fontWeight: "bold" }}>
          Created By:
        </Typography>
        <Typography variant="body1">{url.createdBy}</Typography>
      </Box>
    </Container>
  );
};

export default DetailedInfoForm;
