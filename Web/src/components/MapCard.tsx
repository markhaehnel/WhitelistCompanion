import { ExternalLinkIcon } from "@chakra-ui/icons";
import { Box, Flex, Link, Spacer, Text } from "@chakra-ui/layout";
import { IconButton, Spinner, Tooltip, useDisclosure } from "@chakra-ui/react";
import React from "react";
import { useQuery } from "react-query";
import { fetchUiConfig } from "../api";
import { Card } from "./Card";

export function MapCard() {
    const { data, isFetching } = useQuery("uiConfig", fetchUiConfig, {
        retry: 1,
        refetchOnWindowFocus: false,
        refetchInterval: false,
    });

    return (
        <Card>
            <Flex p={4}>
                <Text fontSize="xl">Karte</Text>
                <Spacer />
                {data?.data && (
                    <Tooltip label="Karte in neuem Tab öffnen" fontSize="md">
                        <Link href={data?.data?.mapUri} isExternal>
                            <IconButton
                                aria-label="Karte in neuem Tab öffnen"
                                size="sm"
                                icon={<ExternalLinkIcon />}
                            />
                        </Link>
                    </Tooltip>
                )}
            </Flex>
            {isFetching ? (
                <Flex
                    w="100%"
                    h="265px"
                    alignItems="center"
                    justifyContent="center"
                >
                    <Spinner size="xl" />
                </Flex>
            ) : (
                data && (
                    <Box
                        as="iframe"
                        src={data.data.mapUri}
                        w="100%"
                        h="265px"
                    />
                )
            )}
        </Card>
    );
}
