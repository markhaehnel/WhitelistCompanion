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
```

## Usage

Open the web app at `http://localhost:5000?secret=SuperSecretApiKey`.

## Configuration

Configuration is done by environment variables.

### Available variables

-   ```bash
    # The secret that is needed to authenticate to the companion API
    API__KEY=SuperSecretApiKey
    ```
-   ```bash
    # Hostname of the minecraft server
    MC__HOST=localhost
    ```
-   ```bash
    # RCON port of the minecraft server
    MC__PORT=25575
    ```
-   ```bash
    # RCON password of the minecraft server
    MC__PASSWORD=YourMcRCONPassword
    ```
-   ```bash
    # Azure AD app client id
    AUTH__MICROSOFT__CLIENTID=MyClientId
    ```
-   ```bash
    # Azure AD app client secret
    AUTH__MICROSOFT__CLIENTSECRET=MyClientSecret
    ```
-   ```bash
    # Azure AD registerd callback url
    AUTH__MICROSOFT__REDIRECTURI=https://example.com/auth/callback
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
