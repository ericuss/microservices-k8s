FROM node:13.7.0-alpine 

WORKDIR /app
RUN npm install -g http-server

# Copy package.json and install
COPY Src/Clients/lanreshop/package*.json ./
RUN npm install

# Copy everything else and build
COPY Src/Clients/lanreshop ./
RUN npm run build

EXPOSE 8080
CMD [ "http-server", "dist" ]