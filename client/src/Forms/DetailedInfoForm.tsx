import React, { useEffect, useState } from "react";
import axios from "axios";
import { Container, Typography } from "@mui/material";
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
        <Typography variant="body1" align="center">
          Loading...
        </Typography>
      </Container>
    );
  }

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Typography variant="h5" align="center" sx={{ mb: 2 }}>
        Detailed Information
      </Typography>
      <Typography variant="body1" sx={{ fontWeight: "bold" }}>
        Shortened URL: {url.shortUrl}
      </Typography>
      <Typography variant="body1">
        Original URL: {url.originalUrl}
      </Typography>
      <Typography variant="body1">
        Created Date: {url.createDate}
      </Typography>
      <Typography variant="body1">
        Created By: {url.createdBy}
      </Typography>
    </Container>
  );
};

export default DetailedInfoForm;
