# Use an official Node.js image as the base image
FROM node:14-alpine AS build

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Install dependencies
RUN npm install --silent

# Copy the entire project directory to the working directory
COPY . .

# Build the React app for production
RUN npm run build

# Use Nginx as the base image for serving the static files
FROM nginx:alpine

# Copy the build output from the previous stage to the Nginx web server directory
COPY --from=build /app/build /usr/share/nginx/html

# Expose port 3000 to the outside world
EXPOSE 80

# Start Nginx web server when the container starts
CMD ["nginx", "-g", "daemon off;"]
