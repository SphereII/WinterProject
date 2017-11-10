#!/bin/bash


for each in `ls *.xml`
do
	sed -i 's@<property name="YOffset" value="-8" />@<property name="YOffset" value="-12" />@g' ${each}
	sed -i 's@<property name="YOffset" value="-2" />@<property name="YOffset" value="-12" />@g' ${each}
	sed -i 's@<property name="YOffset" value="-4" />@<property name="YOffset" value="-12" />@g' ${each}
	sed -i 's@<property name="YOffset" value="-5" />@<property name="YOffset" value="-12" />@g' ${each}
	sed -i 's@<property name="YOffset" value="-6" />@<property name="YOffset" value="-12" />@g' ${each}
	sed -i 's@<property name="CopyAirBlocks" value="True" />@<property name="CopyAirBlocks" value="False" />@g' ${each}
	sed -i 's@<property name="ExcludeDistantPOIMesh" value="False" />@<property name="ExcludeDistantPOIMesh" value="True" />@g' ${each}


	if [[ -z `grep -e '"YOffset"' ${each}` ]]; then
		echo "Missing YOffset: ${each}"
		sed -i '/<prefab>/a  <property name="YOffset" value="-12" />' ${each}

	fi
done



