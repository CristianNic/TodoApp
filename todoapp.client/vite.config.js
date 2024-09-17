import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";

export default defineConfig(({ command }) => {

  const isDocker      = env.DOCKER === "true";
  const apiUrl        = env.VITE_API_URL || "http://localhost:5000";
  const isDevelopment = command === "serve";

  let serverConfig = {};

  if (isDocker) {
    // Docker configuration
    serverConfig = {
      port: 3000,
      proxy: {
        "^/todos": {
        target: apiUrl,
        changeOrigin: true,
        secure: false,
        },
      },
    };
  }

 else if (isDevelopment) {
  // Local development configuration
  const baseFolder =
   env.APPDATA !== undefined && env.APPDATA !== ""
    ? `${env.APPDATA}/ASP.NET/https`
    : `${env.HOME}/.aspnet/https`;

  const certificateName = "todoapp.client";
  const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
  const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

  if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (
      0 !==
      child_process.spawnSync(
      "dotnet",
      [
        "dev-certs",
        "https",
        "--export-path",
        certFilePath,
        "--format",
        "Pem",
        "--no-password",
      ],
      { stdio: "inherit" }
      ).status
    ) {
      throw new Error("Could not create certificate.");
    }
  }

  // Define the backend target for the proxy
  const target = env.ASPNETCORE_HTTPS_PORT
    ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
    : env.ASPNETCORE_URLS
    ? env.ASPNETCORE_URLS.split(";")[0]
    : "https://localhost:7266";

  serverConfig = {
    port: 5173,
    https: {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath),
    },
    proxy: {
      "^/todos": {
        target,
        secure: false,
      },
    },
  };
 } else {
  serverConfig = {};
 }

  return {
    plugins: [react()],
      resolve: {
        alias: {
        "@": "/src",
        },
    },
    server: serverConfig, 
      build: {
        outDir: "dist", 
      },
  };
});
