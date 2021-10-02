import { useColorModeValue } from "@chakra-ui/color-mode";
import Icon from "@chakra-ui/icon";
import * as React from "react";

export function Logo() {
    return (
        <Icon boxSize={20} viewBox="0 0 887 1024" fill="white">
            <path d="M443.407-.01L.002 256.007V768l443.405 255.996L886.812 768.01V256.007zm0 93.113l362.786 209.425v418.911L443.407 930.884 80.621 721.459V302.538z"></path>
            <path d="M63.17 241.662l-45.708 80.002 403.095 230.34 45.708-80.002z"></path>
            <path d="M823.643 241.662l-403.095 230.34 45.718 80.002 403.085-230.34z"></path>
            <path d="M397.339 511.993v460.681h92.136V511.993z"></path>
        </Icon>
    );
}
