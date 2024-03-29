# WhitelistCompanion

WhitelistCompanion is an app that acts as a whitelist management tool for semi-private (user-invite) minecraft servers.

Users can send a link to another user where they can whitelist themselves.

## Installation

### Docker CLI

```bash
docker run -it -p 5000:5000 \
    -e APIKEY=SuperSecretApiKey \
    -e MC_HOST=localhost\
    -e MC_PORT=25575\
    -e MC_PASSWORD=YourMcRCONPassword \
    ghcr.io/markhaehnel/whitelist-companion:main
```

### Docker-Compose

```yml
version: "3"
services:
    whitelistcompanion:
        image: ghcr.io/markhaehnel/whitelist-companion:main
        ports:
            - "5000:5000"
        environment:
            API__KEY: SuperSecretApiKey
            MC__HOST: localhost
            MC__PORT: 25575
            MC__PASSWORD: YourMcRCONPassword
            AUTH__MICROSOFT__CLIENTID: "MyClientId"
            AUTH__MICROSOFT__CLIENTSECRET: "MyClientSecret"
            AUTH__MICROSOFT__REDIRECTURI: "https://example.com/auth/callback"
            UI__SERVERADDRESS: "mc.example.com"
            UI__MAPURI: "https://dynmap.example.com"
            UI__MAPPREVIEWURI: "https://dynmap.example.com/?nogui=true&zoom=3"
```

## Usage

Open the web app at `http://localhost:5000?secret=SuperSecretApiKey`.

## Configuration

Configuration is done by environment variables.

### Available variables

#### API

```bash
# The secret that is needed to authenticate to the companion API
API__KEY=SuperSecretApiKey
```

#### RCON

```bash
# Hostname of the minecraft server
MC__HOST=localhost

# RCON port of the minecraft server
MC__PORT=25575

# RCON password of the minecraft server
MC__PASSWORD=YourMcRCONPassword
```

#### Authentication

```bash
# Azure AD app client id
AUTH__MICROSOFT__CLIENTID=MyClientId

# Azure AD app client secret
AUTH__MICROSOFT__CLIENTSECRET=MyClientSecret

# Azure AD registerd callback url
AUTH__MICROSOFT__REDIRECTURI=https://example.com/auth/callback
```

#### UI

```bash
# Public minecraft server address
UI__SERVERADDRESS=mc.example.com

# Public url of your map server (e.g. dynmap)
UI__MAPURI=https://dynmap.example.com

# Public preview url of your map server (e.g. dynmap with nogui and zoom options)
# This is shown on the map card
UI__MAPPREVIEWURI=https://dynmap.example.com/?nogui=true&zoom=3
```

## FAQ

### How does authentication with work?

Authentication with Microsoft accounts is explained [here](https://wiki.vg/Microsoft_Authentication_Scheme).
More about the Mojang authentication can be found [here](https://wiki.vg/Authentication).

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

Created by Mark Hähnel and released under the terms of the [MIT](https://choosealicense.com/licenses/mit/)
