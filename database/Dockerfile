FROM mongo:6.0

# Install wget and gnupg for adding the MongoDB GPG key
RUN apt-get update && apt-get install -y wget gnupg

# Add MongoDB GPG key
RUN wget -qO - https://www.mongodb.org/static/pgp/server-6.0.asc | apt-key add -

# Add MongoDB repository
RUN echo "deb [ arch=amd64,arm64 ] https://repo.mongodb.org/apt/ubuntu focal/mongodb-org/6.0 multiverse" | tee /etc/apt/sources.list.d/mongodb-org-6.0.list

# Update package lists again
RUN apt-get update

# Install mongosh
RUN apt-get install -y mongodb-mongosh

# Clean up to reduce image size
RUN rm -rf /var/lib/apt/lists/*
